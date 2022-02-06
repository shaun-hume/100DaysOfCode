import { Button } from "react-bootstrap";
import {useState} from 'react';

function List() {
    let [tasks] = useState([]);
    tasks = ListTasks();
    return (
        <>{tasks}</>
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


export default List