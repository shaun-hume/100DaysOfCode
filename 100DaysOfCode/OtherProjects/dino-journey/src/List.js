import { Button } from "react-bootstrap";
import {useEffect, useState} from 'react';

function List(tasks) {
    if (tasks != null) {
        return (
            <ul>
                {tasks.tasks.map(task => (
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


export default List