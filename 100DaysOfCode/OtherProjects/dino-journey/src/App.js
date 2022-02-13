import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import { useSpring, animated } from 'react-spring';
import {Row, Col, Container} from 'react-bootstrap';
import List from './List';
import TaskEntry from './TaskEntry';

function SpringDemo() {
  const [state, toggle] = useState(true)
  let tasksFromLocalStorage = JSON.parse(localStorage.getItem('tasks'));
  const [ tasks, setToDoList ] = useState(tasksFromLocalStorage);
  const { x } = useSpring({ from: { x: 0 }, x: state ? 1 : 0, config: { duration: 1000 } })
  return (

    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <Row>
          <Col>
            <TaskEntry></TaskEntry>
          </Col>
          <Col>
            <List tasks={tasks} />
          </Col>
        </Row>
    </div>
  )
}

export default SpringDemo

