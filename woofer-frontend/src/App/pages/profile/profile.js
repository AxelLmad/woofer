import React from 'react';
import Publisher from "../../shared_components/publisher/publisher";
import Feed from "../../shared_components/feed/feed";
import AsideBar from "./aside-bar/aside-bar";
import {lsUserKey} from "../../constants/keys";
import {
    devRootURL,
    followedCommunityApiURLs,
    followedUserApiURLs, postApiURLs,
    userApiURLs, userPictureApiURLs
} from "../../constants/api-url";
import {Post} from "../../../models/post";
import firebase from "firebase";


class Profile extends React.Component{

    state = {
        followers : [],
        openModal: false,
        selectedList: [],
        posts: [],
        picture: ''
    };


    constructor(){
        super();

        const userId = JSON.parse(localStorage.getItem(lsUserKey)).id;

        fetch(`${devRootURL}${userApiURLs.getById(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{
                this.setState({
                    nickname: json.nickName,
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



                                    })
                                    .catch(err => console.log(err));

                            })
                            .catch(err => console.log(err));

                    })
                    .catch(err => console.log(err));

            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${postApiURLs.getUserPosts(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                let postList = json.map(
                    (post) => {

                        return new Post(post.id,
                            post.content,
                            post.creationDate,
                            {id: post.authorId, name: post.nickName, nickname: post.nickName},
                            {id: post.communityId, name: post.communityName}
                            );

                    }
                );

                this.setState({
                    posts: [...postList]
                });


            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${userPictureApiURLs.byUserId(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                this.setState({
                    picture: json.serverPath
                });


            })
            .catch(err => console.log(err));

        let nullPicture = false;

        const acc = JSON.parse(localStorage.getItem(lsUserKey));

        fetch(`${devRootURL}${userPictureApiURLs.byUserId(acc.id)}`,{
            method: 'GET'
        })
            .then(response => response.status===200?response.json():nullPicture = true)
            .then((json)=>{
                if(!nullPicture){

                    const storageRef = firebase.storage().ref();
                    const starsRef = storageRef.child(json.serverPath);

                    starsRef.getDownloadURL().then((url) => {
                        this.setState({picture: url});
                    }).catch(function(error) {

                        switch (error.code) {
                            case 'storage/object-not-found':
                                console.log('Object does not exist');
                                break;
                        }
                    });
                }
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
                    <Feed posts={this.state.posts}/>
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
