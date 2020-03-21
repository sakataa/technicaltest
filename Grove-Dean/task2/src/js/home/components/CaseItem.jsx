import React from 'react';
import { Card, CardHeader, CardBody, CardText } from 'reactstrap';

const CaseItem = (props) => {
    const { item } = props;
    return (
        <Card key={item.country}>
            <CardHeader>{item.country}</CardHeader>
            <CardBody>
                <CardText>Total Cases: {item.cases}</CardText>
                <CardText>Today ({new Date().toLocaleDateString()}) Cases: {item.todayCases}</CardText>
                <CardText>Deaths: {item.deaths}</CardText>
                <CardText>Today Deaths: {item.todayDeaths}</CardText>
                <CardText>Recovered: {item.recovered}</CardText>
            </CardBody>
        </Card>
    )
}

export default CaseItem;