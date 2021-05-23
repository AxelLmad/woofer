import React from 'react';
import Publisher from "../../shared_components/publisher/publisher";
import Feed from "../../shared_components/feed/feed";
import AsideBar from "./aside-bar/aside-bar";
import {communityApiURLs, devRootURL, postApiURLs} from "../../constants/api-url";

class Communities extends React.Component{

    state = {

        posts: []

    }

    constructor() {
        super();

    }

    render(){

        return <div className={"flex flex-row justify-center w-full xl:ml-72"}>
                <div className={"flex flex-col items-center w-full mt-20"}>
                    <Publisher className={"w-full"} username={JSON.parse(localStorage.getItem('woofer-user-ac')).acc}/>
                    <Feed posts={this.state.posts}/>
                </div>
                <AsideBar className={"hidden"}/>
            </div>



    }

}

export default Communities;
