import React, { useState, useEffect, useRef } from "react";
import CaseByCountryList from './CaseByCountryList';
import Overview from './Overview';
import Filter from './Filter';
import axios from 'axios';

const API_DATA_OVERVIEW_URL = 'https://corona.lmao.ninja/all';
const API_DATA_BYCOUNTRY_URL = 'https://corona.lmao.ninja/countries';
const defaultImportantItem = {
    country: 'Viet Nam',
    countryInfo: {iso2: 'VN', flag: 'https://raw.githubusercontent.com/NovelCOVID/API/master/assets/flags/vn.png'},
    cases: 0,
    todayCases: 0,
    deaths: 0,
    todayDeaths: 0,
    recovered: 0
}
const DEFAULT_TIMER = 20;

const AppContainer = () => {
    const [overviewData, setOverviewData] = useState(null);
    const [dataByCountry, setDataByCountry] = useState([]);
    const [filteredText, setFilteredText] = useState('');
    const [refreshTime, setRefreshTime] = useState(DEFAULT_TIMER);
    const [importantItem, setImportantItem] = useState(Object.assign(defaultImportantItem));
    const timeoutId = useRef(null);

    const getData = () => {
        axios.get(API_DATA_OVERVIEW_URL).then(response => {
            setOverviewData(response.data);
        })

        const apiByCountry = API_DATA_BYCOUNTRY_URL + '/' + encodeURIComponent(importantItem.country);
        axios.get(apiByCountry).then(response => {
            const { cases, todayCases, deaths, todayDeaths, recovered } = response.data;
            setImportantItem(Object.assign({}, importantItem, { cases, todayCases, deaths, todayDeaths, recovered }));
        })

        axios.get(API_DATA_BYCOUNTRY_URL)
            .then(response => {
                const myFavoriteCountry = response.data.find(x => x.countryInfo && x.countryInfo.iso2 === importantItem.countryInfo.iso2);
                if(myFavoriteCountry && myFavoriteCountry.country !== importantItem.country){
                    setImportantItem(Object.assign({}, importantItem, myFavoriteCountry))
                }
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
        timeoutId.current = window.setTimeout(refresh, timeInMillisecond);
    }

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
            <Overview overviewData={overviewData} importantItem={importantItem} onChangeTimer={handleChangeTimer} selectedTime={refreshTime} />
            <Filter onSearchChange={handleChangeSearch} />
            <CaseByCountryList dataByCountry={displayDataList} />
        </div>
    );
}

export default AppContainer;