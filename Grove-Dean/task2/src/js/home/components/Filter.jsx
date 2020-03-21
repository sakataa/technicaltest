import React, { useState } from 'react';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

const Filter = (props) => {
    const { onSearchChange } = props;
    const [searchedText, setSearchedText] = useState('');

    const onChange = (event) => {
        const { value } = event.target;
        setSearchedText(value);
        onSearchChange && onSearchChange(value);
    }

    return (
        <Form tag="div">
            <FormGroup row>
                <Label for="search" sm={2}>Search By Country</Label>
                <Col sm={10}>
                    <Input type="text" name="search" id="search" value={searchedText} onChange={onChange} />
                </Col>
            </FormGroup>
        </Form>
    )
}

export default Filter;