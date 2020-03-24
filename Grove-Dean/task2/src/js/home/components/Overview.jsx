import React, {useContext} from 'react';
import PropTypes from 'prop-types';
import { Jumbotron, Alert } from 'reactstrap';
import { formatNumber } from '../../utils/fomatter';
import TimeSelection from './TimeSelection';
import { getText } from '../../resources/resourceManager';
import { LanguageContext } from '../../utils/context';

const Overview = (props) => {
    const { overviewData, selectedTime, onChangeTimer } = props;
    const langKey = useContext(LanguageContext);

    return (
        <div className="my-3">
            <Jumbotron fluid className="text-center py-3">
                <h5 className="">{getText(langKey, 'pageHeading')}</h5>
                <TimeSelection selectedTime={selectedTime} onChangeTimer={onChangeTimer} />

                <Alert color="primary">
                    {getText(langKey, 'coronavirusCases')}: {overviewData && formatNumber(overviewData.cases)}
                </Alert>
                <Alert color="success">
                    {getText(langKey, 'recovered')}: {overviewData && formatNumber(overviewData.recovered)}
                </Alert>
                <Alert color="danger">
                    {getText(langKey, 'deaths')}: {overviewData && formatNumber(overviewData.deaths)}
                </Alert>
            </Jumbotron>
        </div>
    );
};

Overview.propTypes = {
    overviewData: PropTypes.object,
    selectedTime: PropTypes.number.isRequired,
    onChangeTimer: PropTypes.func
}

export default Overview;