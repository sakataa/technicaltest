import React from 'react';
import PropTypes from 'prop-types';
import { Card, CardHeader, CardBody, CardText } from 'reactstrap';
import { formatNumber } from '../../utils/fomatter';

const CaseItem = (props) => {
    const { item } = props;
    return (
        <Card key={item.country}>
            <CardHeader>{item.country}</CardHeader>
            <CardBody>
                <CardText>Total Cases: {formatNumber(item.cases)}</CardText>
                <CardText>Today ({new Date().toLocaleDateString()}) Cases: {formatNumber(item.todayCases)}</CardText>
                <CardText>Deaths: {formatNumber(item.deaths)}</CardText>
                <CardText>Today Deaths: {formatNumber(item.todayDeaths)}</CardText>
                <CardText>Recovered: {formatNumber(item.recovered)}</CardText>
            </CardBody>
        </Card>
    )
}

CaseItem.propTypes = {
    item: PropTypes.object.isRequired
}

export default CaseItem;