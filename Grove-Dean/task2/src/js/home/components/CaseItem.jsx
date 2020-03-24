import React, { useContext } from 'react';
import PropTypes from 'prop-types';
import { Card, CardHeader, CardBody, CardText } from 'reactstrap';
import { formatNumber } from '../../utils/fomatter';
import { getText } from '../../resources/resourceManager';
import { LanguageContext } from '../../utils/context';

const CaseItem = (props) => {
    const { item } = props;
    const langKey = useContext(LanguageContext);

    return (
        <Card key={item.country}>
            <CardHeader><img className="mr-2" alt={item.countryInfo.iso2} src={item.countryInfo.flag} with="30" height="20" />{item.country}</CardHeader>
            <CardBody>
                <CardText>{getText(langKey, 'totalCases')}: {formatNumber(item.cases)}</CardText>
                <CardText>{getText(langKey, 'todayCases').replace('{0}', new Date().toLocaleDateString())}: {formatNumber(item.todayCases)}</CardText>
                <CardText>{getText(langKey, 'deaths')}: {formatNumber(item.deaths)}</CardText>
                <CardText>{getText(langKey, 'todayDeaths')}: {formatNumber(item.todayDeaths)}</CardText>
                <CardText>{getText(langKey, 'recovered')}: {formatNumber(item.recovered)}</CardText>
            </CardBody>
        </Card>
    )
}

CaseItem.propTypes = {
    item: PropTypes.object.isRequired
}

export default CaseItem;