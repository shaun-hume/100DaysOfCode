import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import { useSpring, animated } from 'react-spring';
import {Row, Col, Container} from 'react-bootstrap';
import List from './List';
import TaskEntry from './TaskEntry';

function SpringDemo() {
  const [state, toggle] = useState(true)
  const { x } = useSpring({ from: { x: 0 }, x: state ? 1 : 0, config: { duration: 1000 } })
  localStorage.setItem('myData', Date.now());
  return (

    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <Row>
          <Col>
            <TaskEntry></TaskEntry>
          </Col>
          <Col>
            <List></List>
          </Col>
        </Row>
    </div>
  )
}

export default SpringDemo

