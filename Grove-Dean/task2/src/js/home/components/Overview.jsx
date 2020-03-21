import React from 'react';
import { Jumbotron, Alert, Form, FormGroup, Label, Col, Input } from 'reactstrap';

const Overview = (props) => {
    const { overviewData, selectedTime, onChangeTimer } = props;

    const handleChangeTimer = (event) => {
        const changedTime = Number(event.target.value);
        if(changedTime !== selectedTime){
            onChangeTimer && onChangeTimer(changedTime);
        }
    }

    if (!overviewData) {
        return null;
    }

    return (
        <div>
            <Jumbotron fluid className="text-center py-3">
                <h5 className="">COVID-19 CORONAVIRUS OUTBREAK</h5>
                <Form tag="div" className="justify-content-center my-3">
                    <FormGroup row className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="timeDropdown" sm={2}>Refresh After(s):</Label>
                        <Col sm={1}>
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
                <Alert color="primary">
                    Coronavirus Cases: {overviewData.cases}
                </Alert>
                <Alert color="success">
                    Recovered: {overviewData.recovered}
                </Alert>
                <Alert color="danger">
                    Deaths: {overviewData.deaths}
                </Alert>
            </Jumbotron>
        </div>
    );
};

export default Overview;