import React, { useState } from "react";
import Header from "./layout/Header";
import Content from "./layout/Content";
import Home from "./home";

const App = () => {
    return(
        <div>
            <Header />
            <Content>
                <Home />
            </Content>
        </div>
    )
}

export default App;