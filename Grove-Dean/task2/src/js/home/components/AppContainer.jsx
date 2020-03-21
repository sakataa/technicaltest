import React, { useState, useEffect, useRef } from "react";
import CaseByCountryList from './CaseByCountryList';
import Overview from './Overview';
import Filter from './Filter';
import axios from 'axios';

const API_DATA_OVERVIEW_URL = 'https://corona.lmao.ninja/all';
const API_DATA_BYCOUNTRY_URL = 'https://corona.lmao.ninja/countries';
const DEFAULT_TIMER = 10;

const App = () => {
    const [overviewData, setOverviewData] = useState(null);
    const [dataByCountry, setDataByCountry] = useState([]);
    const [filteredText, setFilteredText] = useState('');
    const [refreshTime, setRefreshTime] = useState(DEFAULT_TIMER);
    const timeoutId = useRef(null);

    const getData = () => {
        axios.get(API_DATA_OVERVIEW_URL).then(response => {
            setOverviewData(response.data);
        })

        axios.get(API_DATA_BYCOUNTRY_URL)
            .then(response => {
                setDataByCountry(response.data);
            });
    }

    const refresh = () => {
        const currentTimeoutId = timeoutId.current;
        if(currentTimeoutId !== null){
            window.clearTimeout(timeoutId);
        }
        
        getData();
        const timeInMillisecond = refreshTime * 1000;
        console.log(refreshTime);
        timeoutId.current = window.setTimeout(refresh, timeInMillisecond);
    }

    useEffect(() => {
        getData();
    }, [])

    useEffect(() => {
        refresh();

        return () => window.clearTimeout(timeoutId.current);
    }, [refreshTime])

    const handleChangeSearch = (searchedText) => {
        setFilteredText(searchedText);
    }

    const handleChangeTimer = (selectedTime) => {
        setRefreshTime(selectedTime);
    }

    const displayDataList = filteredText ? 
                    dataByCountry.filter(item => {
                        return item.country.toLowerCase().indexOf(filteredText.toLowerCase()) !== -1;
                    }) :
                    dataByCountry;

    return (
        <div>
            <Overview overviewData={overviewData} onChangeTimer={handleChangeTimer} selectedTime={refreshTime} />
            <Filter onSearchChange={handleChangeSearch} />
            <CaseByCountryList dataByCountry={displayDataList} />
        </div>
    );
}

export default App;