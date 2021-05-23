import React from 'react';
import Publisher from "../../shared_components/publisher/publisher";
import Feed from "../../shared_components/feed/feed";
import AsideBar from "./aside-bar/aside-bar";
import {communityApiURLs, devRootURL, postApiURLs} from "../../constants/api-url";
import {Post} from "../../../models/post";
import {lsUserKey} from "../../constants/keys";

class Communities extends React.Component{

    state = {

        posts: []

    }

    constructor() {
        super();



        fetch(`${devRootURL}${postApiURLs.followedCommunity(JSON.parse(localStorage.getItem(lsUserKey)).id)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                const auxPosts = json.map((element) => {

                    return new Post(element.id,
                        element.content,
                        element.creationDate,
                        {id: element.authorId, name: element.name, nickname: element.nickName},
                        {name: element.communityName, id: element.communityId, color: element.color});

                });

                this.setState({posts: [...auxPosts]});
            })
            .catch(err => console.log(err));

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
