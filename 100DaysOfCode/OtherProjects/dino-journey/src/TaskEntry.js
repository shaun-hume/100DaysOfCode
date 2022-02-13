import { Button } from "react-bootstrap";
import React from "react";
import { useState } from 'react';

function AddTask(inputValue) {
    //get tasks from local storage then add new task to it then save it back to local storage
    let currentTasks = JSON.parse(localStorage.getItem('tasks'));
    if (currentTasks != null) {
        currentTasks.push(inputValue);
        localStorage.setItem('tasks', JSON.stringify(currentTasks));
    }
    else {
        currentTasks = [inputValue];
        localStorage.setItem('tasks', JSON.stringify(currentTasks));
    }
    // tasks = currentTasks;
}

function TaskEntry() {
    let [tasks] = useState([]);
    const [inputValue, setInputValue] = React.useState("");
    const onChangeHandler = event => {
        setInputValue(event.target.value);
    };
    tasks = ListTasks();
    return (
        //Add a form for adding a new task
        <form>
            <input onChange={onChangeHandler} value={inputValue} type="text" placeholder="Add a new task" />
            <Button onClick={() => AddTask(inputValue)}>Add</Button>
        </form>
    )
}

//get tasks from local storage and display them as a list
function ListTasks() {
    let tasks = JSON.parse(localStorage.getItem('tasks'));
    if (tasks != null) {
        return (
            <ul>
                {tasks.map(task => (
                    <li>{task}</li>
                ))}
            </ul>
        )
    }
    else {
        return (
            <ul>
                <li>No tasks</li>
            </ul>
        )
    }
}

function UpdateValue(evt, inputValue) {
    inputValue = evt.target.value;
}

// //Add a new task to the list
// // localStorage.clear();
// let currentTasks = localStorage.getItem('tasks') == null ? [] :localStorage.getItem('tasks');
// console.log(typeof currentTasks);
// console.log(currentTasks);

// // let currentTasks = ["First Task"];
// currentTasks.concat(this.state.inputValue);
// console.log(currentTasks);
// localStorage.setItem('tasks', currentTasks);
// }

export default TaskEntry