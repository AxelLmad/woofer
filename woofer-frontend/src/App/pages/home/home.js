import React from 'react';
import {Post} from "../../../models/post";
import Feed from "../../shared_components/feed/feed";
import Publisher from "../../shared_components/publisher/publisher";
import AsideBar from "./aside-bar/aside-bar";
import {devRootURL, postApiURLs} from "../../constants/api-url";
import {currentIP, lsUserKey} from "../../constants/keys";

class Home extends React.Component{

    state = {
        posts: []
    }

    constructor() {
        super();

        const userNickname = JSON.parse(localStorage.getItem('woofer-user-ac'));

        if (userNickname === null || userNickname === undefined || userNickname.acc === ''){

            window.location.href = `http://${currentIP}/login`;

        }

        const userId = JSON.parse(localStorage.getItem(lsUserKey)).id;

        fetch(`${devRootURL}${postApiURLs.getLastPosts(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                const auxPosts = json.map((element) => {

                    return new Post(element.id,
                        element.content,
                        element.creationDate,
                        {id: element.authorId, name: element.name, nickname: element.nickName},
                        {name: element.communityName, id: element.communityId});

                });

                this.setState({posts: [...auxPosts]});
            })
            .catch(err => console.log(err));
    }

    render(){

        return(
            <div className={"flex flex-row justify-center w-full xl:ml-72"}>
                <div className={"flex flex-col items-center w-full mt-20"}>
                    <Publisher className={"w-full"} username={JSON.parse(localStorage.getItem('woofer-user-ac')).acc}/>
                    <Feed posts={this.state.posts}/>
                </div>
                <AsideBar className={"hidden"}/>
            </div>
        );

    }

}

export default Home;
