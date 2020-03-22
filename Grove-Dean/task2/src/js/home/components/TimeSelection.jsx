import React from 'react';
import PropTypes from 'prop-types';
import { Form, FormGroup, Label, Col, Input } from 'reactstrap';

const TimeSelection = (props) => {
    const { selectedTime, onChangeTimer } = props;

    const handleChangeTimer = (event) => {
        const changedTime = Number(event.target.value);
        if (changedTime !== selectedTime) {
            onChangeTimer && onChangeTimer(changedTime);
        }
    }

    return (
        <Form tag="div" className="justify-content-center my-3 px-4">
            <FormGroup row className="mb-2 mr-sm-2 mb-sm-0">
                <Label for="timeDropdown" xs={6} md={4} lg={3} xl={2}>Refresh After(second):</Label>
                <Col xs={6} md={3} lg={2} xl={1}>
                    <Input type="select" name="timeDropdown" id="timeDropdown" value={selectedTime} onChange={handleChangeTimer}>
                        <option>10</option>
                        <option>20</option>
                        <option>30</option>
                        <option>40</option>
                        <option>50</option>
                    </Input>
                </Col>
            </FormGroup>
        </Form>
    )
}

TimeSelection.propTypes = {
    selectedTime: PropTypes.number.isRequired,
    onChangeTimer: PropTypes.func
}

export default TimeSelection;