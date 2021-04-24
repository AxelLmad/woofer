import React from 'react';
import Header from "./header/header";
import Sidebar from "./sidebar/sidebar";

const layout = (props) => {

    return(

        <div>
            <Header/>
            <div className={"flex flex-row justify-between"}>
            <Sidebar/>
            {props.children}
            </div>
        </div>

    );

};

export default layout;
