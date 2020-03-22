import React, { useState } from 'react';
import PropTypes from 'prop-types';
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
                <Label for="search" sm={5} md={3} lg={2}>Search By Country</Label>
                <Col sm={7} md={9} lg={10}>
                    <Input type="text" name="search" id="search" value={searchedText} onChange={onChange} />
                </Col>
            </FormGroup>
        </Form>
    )
}

Filter.propTypes = {
    onSearchChange: PropTypes.func
}

export default Filter;