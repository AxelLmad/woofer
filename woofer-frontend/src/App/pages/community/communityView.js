import React from 'react';
import Publisher from "../../shared_components/publisher/publisher";
import Feed from "../../shared_components/feed/feed";
import AsideBar from "../communities/aside-bar/aside-bar";
import {
    communityApiURLs,
    devRootURL,
    followedCommunityApiURLs,
    followedUserApiURLs,
    postApiURLs
} from "../../constants/api-url";
import {Post} from "../../../models/post";
import {lsUserKey} from "../../constants/keys";
import firebase from "firebase";
import AlertDialog from "../../shared_components/alertDialog/alertDialog";

class CommunityView extends React.Component{

    state = {

        posts: []

    }

    constructor(props) {
        super(props);

        this.initializeEverything = this.initializeEverything.bind(this);
        this.follow = this.follow.bind(this);

        this.initializeEverything();

    }

    follow() {

        const postBody = JSON.stringify({

                followerId: JSON.parse(localStorage.getItem(lsUserKey)).id,
                communityId: this.props.id
            }
        );

        fetch(`${devRootURL}${followedCommunityApiURLs.follow}`, {
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => {
                if (response.status === 200) {

                    this.setState({openFollowed: true, openFollowedContent: `Has seguido ${this.props.name}`});

                }
                else{

                    fetch(`${devRootURL}${followedCommunityApiURLs.unfollow}`, {
                        method: 'DELETE',
                        headers: {'Content-type': 'application/json;charset=UTF-8'},
                        body: postBody
                    })
                        .then(response => {
                            if (response.status === 200) {

                                this.setState({openFollowed: true, openFollowedContent: `Has dejado de seguir ${this.props.name}`});

                            }
                            else{



                            }
                        }).then(()=>{})
                        .catch(err => console.log(err));

                }
            }).then(()=>{})
            .catch(err => console.log(err));

    }

    initializeEverything() {

        fetch(`${devRootURL}${postApiURLs.communityPosts(this.props.id)}`,{
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

        fetch(`${devRootURL}${communityApiURLs.byId(this.props.id)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                this.setState({
                    description: json.description,
                    color: json.color
                });

                const storageRef = firebase.storage().ref();
                const starsRef = storageRef.child(json.picture);

                starsRef.getDownloadURL().then((url) => {
                    this.setState({picture: url});
                }).catch(function(error) {

                    switch (error.code) {
                        case 'storage/object-not-found':
                            console.log('Object does not exist');
                            break;
                    }
                });


            })
            .catch(err => console.log(err));

    }

    render(){

        return <div className={"flex flex-row justify-center w-full xl:ml-72"}>
            <div className={"flex flex-col items-center w-full mt-20"}>
                <article className={"flex flex-row lg:flex-column bg-dark w-11/12 lg:w-2/5 text-white border rounded p-2"}>
                    <div className={"flex flex-column"}>
                        <figure className={"flex w-24 h-24 lg:w-36 object-contain justify-end mr-5"}>
                            <img className={"max-h-40 p-0.5 border border-primary border-lg"} src={this.state.picture} alt={this.state.nickname}/>
                        </figure>
                    </div>
                    <div className={"flex flex-column"}>
                        <hgroup className={"w-full ml-5"}>
                            <h2 className={"font-bold text-3xl text-primary"}>{this.props.name}</h2>
                            <h4 className={`px-5 ${this.state.color} mr-2 `}>{this.state.description}</h4>
                        </hgroup>

                        <AlertDialog content={this.state.openFollowedContent} open={this.state.openFollowed} handleClose={()=>{this.setState({openFollowed: false})}}/>
                    </div>
                    <button onClick={this.follow}
                            className={`self-end mr-4 rounded-full w-11/12 h-12 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary`}>
                        Seguir
                    </button>
                </article>
                <Publisher className={"w-full"} username={JSON.parse(localStorage.getItem('woofer-user-ac')).acc}/>
                <Feed posts={this.state.posts}/>
            </div>
            <AsideBar className={"hidden"} reload={this.initializeEverything}/>
        </div>



    }

}

export default CommunityView;
