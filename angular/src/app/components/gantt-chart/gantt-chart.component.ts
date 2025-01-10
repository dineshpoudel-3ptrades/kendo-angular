import { Component } from '@angular/core';
import {
  GanttComponent,
  KENDO_GANTT,
  TaskAddEvent,
  ToolbarPosition,
} from '@progress/kendo-angular-gantt';
import { ButtonModule } from '@progress/kendo-angular-buttons';
import {
  GanttDependency,
  CellClickEvent,
  CellCloseEvent,
  TaskClickEvent,
  TaskEditEvent,
} from '@progress/kendo-angular-gantt';
import { Task, tasks, dependencies } from './heirarchial-data';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-gantt-chart',
  standalone: true,
  imports: [KENDO_GANTT, ButtonModule],
  templateUrl: './gantt-chart.component.html',
  styleUrl: './gantt-chart.component.scss',
})
export class GanttChartComponent {
  public data: Task[] = tasks;
  public dependencies: GanttDependency[] = dependencies;
  public dateTimeFormat = 'dd-MMM-yyyy HH:mm';
  public position: ToolbarPosition = 'top';

  public handleCellClick({ sender, columnIndex, dataItem, isEdited }: CellClickEvent): void {
    if (!isEdited) {
      sender.editCell(dataItem, columnIndex, this.createFormGroup(dataItem));
    }
  }

  public handleCellClose(e: CellCloseEvent): void {
    const { formGroup: taskFormGroup, item } = e;

    if (!taskFormGroup.valid) {
      // Prevent closing the edited cell if the form is invalid.
      e.preventDefault();
    } else {
      const editedItem = taskFormGroup.value;
      const originalItem = { ...e.dataItem };

      // Update the edited item
      Object.assign(e.dataItem, editedItem);

      // Edit the ancestor items accordingly if necessary
      if (this.anyChanged(originalItem, editedItem)) {
        let currentItem = item.parent;

        while (currentItem) {
          const dataItem = currentItem.dataItem;
          const subtasks = dataItem.subtasks;

          dataItem.completionRatio =
            subtasks.reduce((acc, curr) => acc + curr.completionRatio, 0) / subtasks.length;
          dataItem.start = new Date(Math.min(...subtasks.map(t => t.start)));
          dataItem.end = new Date(Math.max(...subtasks.map(t => t.end)));

          currentItem = currentItem.parent;
        }
      }
    }
  }

  public onClick(e: TaskClickEvent): void {
    const taskFormGroup = this.createFormGroup(e.dataItem);

    e.sender.editTask(e.dataItem, taskFormGroup);
  }

  public confirm(gantt: GanttComponent): any {
    gantt.openConfirmationDialog();
  }

  public saveTask({
    taskFormGroup,
    item,
    sender,
    dependencies: updatedDependencies,
  }: TaskEditEvent): void {
    if (!taskFormGroup.valid) {
      // Keep the dialog open for editing if the form is invalid.
      return;
    } else {
      const editedItem = taskFormGroup.value;
      const originalItem = { ...item.dataItem };

      // Update the edited item
      Object.assign(item.dataItem, editedItem);

      // Update the ancestor items accordingly if necessary
      if (this.anyChanged(originalItem, editedItem)) {
        let currentItem = item.parent;

        while (currentItem) {
          const dataItem = currentItem.dataItem;
          const subtasks = dataItem.subtasks;

          dataItem.completionRatio =
            subtasks.reduce((acc, curr) => acc + curr.completionRatio, 0) / subtasks.length;
          dataItem.start = new Date(Math.min(...subtasks.map(t => t.start)));
          dataItem.end = new Date(Math.max(...subtasks.map(t => t.end)));

          currentItem = currentItem.parent;
        }
      }

      // Update dependencies
      const processedDependencies = [...this.dependencies]; // Create new array reference

      // Add newly created dependencies
      if (updatedDependencies.createdItems.length) {
        updatedDependencies.createdItems.map(
          dependency => (dependency.id = Math.floor(Math.random() * 1000) + 100)
        );
        processedDependencies.push(...updatedDependencies.createdItems);
      }

      // Update existing dependencies
      if (updatedDependencies.updatedItems.length) {
        updatedDependencies.updatedItems.forEach(dependency => {
          const positionIndex = processedDependencies.findIndex(dep => dep.id === dependency.id);
          if (positionIndex > -1) {
            processedDependencies.splice(positionIndex, 1, dependency);
          }
        });
      }

      // Handle removed dependencies
      if (updatedDependencies.deletedItems) {
        updatedDependencies.deletedItems.forEach(dependency => {
          const positionIndex = processedDependencies.findIndex(dep => dep.id === dependency.id);
          if (positionIndex > -1) {
            processedDependencies.splice(positionIndex, 1);
          }
        });
      }

      this.dependencies = processedDependencies;

      sender.closeTaskDialog();
    }
  }

  public removeTask({ item }: TaskEditEvent): void {
    // Remove the deleted item from its parent's children
    const parentChildren = item.parent ? item.parent.dataItem.subtasks : this.data;
    const indexOfItemToDelete = parentChildren.findIndex(t => t === item.dataItem);
    parentChildren.splice(indexOfItemToDelete, 1);

    let currentItem = item.parent;

    // Update the ancestor items accordingly
    while (currentItem) {
      const dataItem = currentItem.dataItem;
      const subtasks = dataItem.subtasks;

      dataItem.completionRatio =
        subtasks.reduce((acc, curr) => acc + curr.completionRatio, 0) / subtasks.length;
      dataItem.start = new Date(Math.min(...subtasks.map(t => t.start)));
      dataItem.end = new Date(Math.max(...subtasks.map(t => t.end)));

      currentItem = currentItem.parent;
    }
  }

  public cancelEditing(e: TaskEditEvent): void {
    e.sender.closeTaskDialog();
  }

  private createFormGroup(dataItem: Task): FormGroup {
    return new FormGroup({
      id: new FormControl(dataItem.id),
      start: new FormControl(dataItem.start, Validators.required),
      end: new FormControl(dataItem.end, Validators.required),
      completionRatio: new FormControl(dataItem.completionRatio),
      title: new FormControl(dataItem.title),
    });
  }

  private anyChanged(item: Task, editedItem: Task) {
    return (
      item.start !== editedItem.start ||
      item.end !== editedItem.end ||
      item.completionRatio !== editedItem.completionRatio
    );
  }

  public onTaskAdd(e: TaskAddEvent): void {
    const newItem = {
      id: Math.floor(Math.random() * 1000) + 100,
      title: 'NEW TASK',
      start: new Date('2014-06-02T00:00:00.000Z'),
      end: new Date('2014-06-02T00:00:00.000Z'),
      completionRatio: 0,
    };

    const parent = e.selectedItem && e.selectedItem.parent;
    const parentCollection = parent ? parent.dataItem.subtasks : this.data;
    const targetIndex = e.selectedItem
      ? parentCollection.findIndex(item => item.id === e.selectedItem.dataItem.id)
      : 0;

    switch (e.actionType) {
      case 'addTask':
        this.data.unshift(newItem);
        break;
      case 'addAbove':
        parentCollection.splice(targetIndex, 0, newItem);
        break;
      case 'addBelow':
        parentCollection.splice(targetIndex + 1, 0, newItem);
        break;
      case 'addChild':
        const item = e.selectedItem.dataItem;
        const children = item.subtasks;
        newItem.start = item.start;
        newItem.end = item.end;

        if (children) {
          children.unshift(newItem);
        } else {
          item.subtasks = [newItem];
          this.data = [...this.data];
        }
        break;
      default:
        break;
    }
  }
}
