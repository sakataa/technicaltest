import React from 'react';
import PropTypes from 'prop-types';
import { Jumbotron, Alert } from 'reactstrap';
import { formatNumber } from '../../utils/fomatter';
import TimeSelection from './TimeSelection';

const Overview = (props) => {
    const { overviewData, selectedTime, onChangeTimer } = props;

    if (!overviewData) {
        return null;
    }

    return (
        <div className="my-3">
            <Jumbotron fluid className="text-center py-3">
                <h5 className="">COVID-19 CORONAVIRUS OUTBREAK</h5>
                <TimeSelection selectedTime={selectedTime} onChangeTimer={onChangeTimer} />

                <Alert color="primary">
                    Coronavirus Cases: {formatNumber(overviewData.cases)}
                </Alert>
                <Alert color="success">
                    Recovered: {formatNumber(overviewData.recovered)}
                </Alert>
                <Alert color="danger">
                    Deaths: {formatNumber(overviewData.deaths)}
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