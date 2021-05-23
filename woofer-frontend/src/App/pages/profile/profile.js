import React from 'react';
import Publisher from "../../shared_components/publisher/publisher";
import Feed from "../../shared_components/feed/feed";
import AsideBar from "./aside-bar/aside-bar";
import {lsUserKey} from "../../constants/keys";
import {
    communityApiURLs,
    devRootURL,
    followedCommunityApiURLs,
    followedUserApiURLs, postApiURLs,
    userApiURLs, userPictureApiURLs
} from "../../constants/api-url";
import {Post} from "../../../models/post";
import firebase from "firebase";
import AlertDialog from "../../shared_components/alertDialog/alertDialog";


class Profile extends React.Component{

    state = {
        followers : [],
        openModal: false,
        selectedList: [],
        posts: [],
        openFollowed: false,
        openFollowedContent: ''

    };


    constructor(props){
        super(props);

        this.follow = this.follow.bind(this);

        const userId = JSON.parse(localStorage.getItem(lsUserKey)).id;

        fetch(`${devRootURL}${userApiURLs.byNickName(this.props.nickname)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{
                this.setState(
                    {id: json[0].id, name: json[0].name, lastName: json[0].lastName, email: json[0].email, nickname: json[0].nickName, picture: json[0].picture}
                );


                fetch(`${devRootURL}${followedUserApiURLs.getFollowers(this.state.id)}`,{
                    method: 'GET'
                })
                    .then(response => response.json())
                    .then((json)=>{
                        this.setState({
                            followers: json
                        });
                        fetch(`${devRootURL}${followedUserApiURLs.getFollowedUsers(this.state.id)}`,{
                            method: 'GET'
                        })
                            .then(response => response.json())
                            .then((json)=>{
                                this.setState({
                                    followedUsers: json
                                });

                                fetch(`${devRootURL}${followedCommunityApiURLs.getByFollower(this.state.id)}`,{
                                    method: 'GET'
                                })
                                    .then(response => response.json())
                                    .then((json)=>{
                                        this.setState({
                                            communities: json
                                        });


                                        fetch(`${devRootURL}${postApiURLs.getUserPosts(this.state.id)}`,{
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
                                                            {id: post.communityId, name: post.communityName, color: post.color}
                                                        );

                                                    }
                                                );

                                                this.setState({
                                                    posts: [...postList]
                                                });


                                            })
                                            .catch(err => console.log(err));

                                        let nullPicture = false;

                                        fetch(`${devRootURL}${userPictureApiURLs.byUserId(this.state.id)}`,{
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

                                    })
                                    .catch(err => console.log(err));

                            })
                            .catch(err => console.log(err));

                    })
                    .catch(err => console.log(err));

            })
            .catch(err => console.log(err));



    }

    follow(){

        const postBody = JSON.stringify({

                followerId: JSON.parse(localStorage.getItem(lsUserKey)).id
            ,
                followedId: this.state.id
            }
        );

        fetch(`${devRootURL}${followedUserApiURLs.follow}`, {
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => {
                if (response.status === 200) {

                    this.setState({openFollowed: true, openFollowedContent: `Has seguido a ${this.props.nickname}`});

                }
                else{

                    fetch(`${devRootURL}${followedUserApiURLs.unfollow}`, {
                        method: 'DELETE',
                        headers: {'Content-type': 'application/json;charset=UTF-8'},
                        body: postBody
                    })
                        .then(response => {
                            if (response.status === 200){
                                this.setState({openFollowed: true, openFollowedContent: `Has dejado de seguir a ${this.props.nickname}`});
                            }
                        }).then(()=>{})
                        .catch(err => console.log(err));

                }
            }).then(()=>{})
            .catch(err => console.log(err));

    }

    render(){

        return(
            <div className={"flex flex-row justify-center w-full xl:ml-72"}>
                <div className={"flex flex-col items-center w-full mt-20"}>

                    <article className={"flex flex-row lg:flex-column bg-dark w-11/12 lg:w-2/5 text-white border rounded p-2"}>
                        <div className={"flex flex-column"}>
                            <figure className={"flex w-24 h-24 lg:w-36 object-contain justify-end mr-5"}>
                                <img className={"max-h-40 p-0.5 border border-primary border-lg"} src={this.state.picture} alt={this.state.nickname}/>
                            </figure>
                        </div>
                        <div className={"flex flex-column"}>
                            <hgroup className={"w-full ml-5"}>
                                <h2 className={"font-bold text-3xl text-primary"}>{this.state.nickname}</h2>
                                <h4 >{this.state.followers.length===0?'':`${this.state.followers.length} seguidores`}</h4>
                                <h4>{this.state.name} {this.state.lastName}</h4>
                            </hgroup>

                            <AlertDialog content={this.state.openFollowedContent} open={this.state.openFollowed} handleClose={()=>{this.setState({openFollowed: false})}}/>
                        </div>
                        <button onClick={this.follow}
                                className={`${(this.state.id === JSON.parse(localStorage.getItem(lsUserKey)).id)?'hidden':''}
                            self-end mr-4 rounded-full w-8/12 h-12 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary`}>
                            Seguir
                        </button>
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
