import React from 'react';
import Publisher from "../../shared_components/publisher/publisher";
import Feed from "../../shared_components/feed/feed";
import AsideBar from "./aside-bar/aside-bar";
import {lsUserKey} from "../../constants/keys";
import {
    devRootURL,
    followedCommunityApiURLs,
    followedUserApiURLs,
    postApiURLs,
    userApiURLs
} from "../../constants/api-url";
import {Post} from "../../../models/post";

class Profile extends React.Component{

    state = {
        followers : [],
        modalSelected: 0, // 0: none, 1: followers, 2: followed users, 3: communities, default: throw error
    };

    constructor(){
        super();

        const userId = JSON.parse(localStorage.getItem(lsUserKey)).id;

        // nested HTTP calls: 1.user info 2.user followers 3.user followed users 4.user followed communities
        fetch(`${devRootURL}${userApiURLs.getById(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{
                this.setState({
                    nickname: json.nickName,
                    picture: json.picture,
                    name: json.name,
                    lastName: json.lastName
                });

                fetch(`${devRootURL}${followedUserApiURLs.getFollowers(userId)}`,{
                    method: 'GET'
                })
                    .then(response => response.json())
                    .then((json)=>{
                        this.setState({
                            followers: json
                        });
                        fetch(`${devRootURL}${followedUserApiURLs.getFollowedUsers(userId)}`,{
                            method: 'GET'
                        })
                            .then(response => response.json())
                            .then((json)=>{
                                this.setState({
                                    followedUsers: json
                                });

                                fetch(`${devRootURL}${followedCommunityApiURLs.getByFollower(userId)}`,{
                                    method: 'GET'
                                })
                                    .then(response => response.json())
                                    .then((json)=>{
                                        this.setState({
                                            communities: json
                                        });

                                        console.log(this.state);

                                    })
                                    .catch(err => console.log(err));

                            })
                            .catch(err => console.log(err));

                    })
                    .catch(err => console.log(err));

            })
            .catch(err => console.log(err));

    }





    render(){

        return(
            <div className={"flex flex-row justify-center w-full xl:ml-72"}>
                <div className={"flex flex-col items-center w-full mt-20"}>

                    <article className={"grid grid-cols-2 gap-x-3 bg-dark w-2/5 text-white border rounded py-2"}>
                        <div className={"flex flex-column"}>
                            <figure className={"flex justify-end"}>
                                <img className={"w-3/4 max-h-40 p-0.5 border border-primary border-lg"} src={this.state.picture} alt={this.state.nickname}/>
                            </figure>
                        </div>
                        <div className={"flex flex-column"}>
                            <hgroup className={"w-full ml-5"}>
                                <h2 className={"font-bold text-3xl text-primary"}>{this.state.nickname}</h2>
                                <h4 >{this.state.followers.length===0?'':`${this.state.followers.length} seguidores`}</h4>
                                <h4>{this.state.name} {this.state.lastName}</h4>
                            </hgroup>
                            <button className={"self-end mr-4 rounded-full w-11/12 h-12 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                                Seguir
                            </button>
                        </div>
                    </article>

                    <Publisher className={"w-full"} username={JSON.parse(localStorage.getItem('woofer-user-ac')).acc}/>
                    <Feed posts={[]}/>
                </div>
                <AsideBar followers={this.state.followers===undefined?[]:[...this.state.followers]}
                          followedUsers={this.state.followedUsers===undefined?[]:[...this.state.followedUsers]}
                          communities={this.state.communities===undefined?[]:[...this.state.communities]}
                    className={"hidden"}/>
            </div>
);

    }

}

export default Profile;
