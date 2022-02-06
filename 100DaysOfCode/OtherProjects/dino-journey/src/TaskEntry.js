import { Button } from "react-bootstrap";
import React from "react";

class TaskEntry extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            inputValue: ''
        };
    }

    AddTask() {
        //get tasks from local storage then add new task to it then save it back to local storage
        let currentTasks = JSON.parse(localStorage.getItem('tasks'));
        if (currentTasks != null) {
            currentTasks.push(this.state.inputValue);
            localStorage.setItem('tasks', JSON.stringify(currentTasks));
        }
        else {
            currentTasks = [this.state.inputValue];
            localStorage.setItem('tasks', JSON.stringify(currentTasks));
        }
        // tasks = currentTasks;
    }

    render() {
        return (
            //Add a form for adding a new task
            <form>
                <input value={this.state.inputValue} type="text" placeholder="Add a new task" onChange={evt => this.updateInputValue(evt)} />
                <Button onClick={() => this.AddTask()}>Add</Button>
            </form>
        )
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

    updateInputValue(evt) {
        const val = evt.target.value;
        // ...
        this.setState({
            inputValue: val
        });
    }
}
export default TaskEntry