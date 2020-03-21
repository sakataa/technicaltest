import React from "react";
import { Row, Col } from 'reactstrap';
import CaseItem from './CaseItem';

const CaseByCountryList = (props) => {
    const {dataByCountry} = props;

    return (
        <Row>
            {
                dataByCountry.map(item => (
                    <Col key={item.country} md={3} className="mb-4">
                        <CaseItem key={item.country} item={item} />
                    </Col>
                ))
            }
        </Row>
    );
}

export default CaseByCountryList;