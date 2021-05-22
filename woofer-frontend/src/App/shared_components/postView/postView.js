import React from 'react';
import atIcon from '../../../img/icon/at-symbol.svg';
import likeIcon from '../../../img/icon/heart.svg';
import dislikeIcon from '../../../img/icon/thumb-down.svg';
import amazingIcon from '../../../img/icon/lightning-bolt.svg';
import {devRootURL, postApiURLs, postPictureApiURLs, reactionApiURLs} from "../../constants/api-url";
import firebase from "firebase";
import {Modal} from "@material-ui/core";
import CommentBox from "../commentBox/commentBox";
import {lsUserKey} from "../../constants/keys";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import Feed from "../feed/feed";
import {Post} from "../../../models/post";
import backIcon from "../../../img/icon/arrow-left.svg";


class PostView extends React.Component{

    state = {

        picture: null,
        commentModal: false,
        repliesModal: false,
        heartReactions: 0,
        thunderReactions: 0,
        thumbDownReactions: 0,
        anchorEl: null,
        replies: []

    }

    constructor(props) {
        super(props);

        this.handleCommentChange = this.handleCommentChange.bind(this);
        this.commentPublish = this.commentPublish.bind(this);
        this.handleClick = this.handleClick.bind(this);
        this.handleClose = this.handleClose.bind(this);

        let nullPicture = false;

        fetch(`${devRootURL}${postApiURLs.getReplies(this.props.id)}`,{
            method: 'GET'
        })
            .then(response => response.status===200?response.json():nullPicture = true)
            .then((json)=>{

                const auxPosts = json.map((element) => {

                    return new Post(element.id,
                        element.content,
                        element.creationDate,
                        {id: element.authorId, name: element.name, nickname: element.nickName},
                        {name: element.communityName, id: element.communityId});

                });

                this.setState({replies: [...auxPosts]})

            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${reactionApiURLs.byId(this.props.id)}`,{
            method: 'GET'
        })
            .then(response => response.status===200?response.json():nullPicture = true)
            .then((json)=>{
                this.setState({
                    heartReactions: json.type1,
                    thunderReactions: json.type2,
                    thumbDownReactions: json.type3});

            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${postPictureApiURLs.byPostId(this.props.id)}`,{
            method: 'GET'
        })
            .then(response => response.status===200?response.json():nullPicture = true)
            .then((json)=>{
                if(!nullPicture){

                    const storageRef = firebase.storage().ref();
                    const starsRef = storageRef.child(json[0].serverPath);

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

    handleClick(event) {
        this.setState({anchorEl: event.currentTarget})
    };

    handleClose () {
        this.setState({anchorEl: null});
    };

    handleCommentChange(){this.setState({commentModal: !this.state.commentModal});}

    commentPublish(postBody){

        fetch(`${devRootURL}${postApiURLs.create}`, {
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => {
                if (response.status === 200) {

                    this.handleCommentChange();

                }
            })
            .catch(err => console.log(err));

    }

    handleReaction(type){

        switch (type){



        }

        this.setState({})

        const acc = JSON.parse(localStorage.getItem(lsUserKey));

        const postBody = JSON.stringify({
            type: type,
            userId: acc.id,
            postId: this.props.id,
        });

        fetch(`${devRootURL}${reactionApiURLs.set}`, {
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => {
                if (response.status === 200) {

                    console.log('success');

                }
            })
            .catch(err => console.log(err));

    }

    render() {

        return(
            <section className={`border rounded border-white flex flex-col-reverse md:flex-row-reverse justify-end bg-dark pt-8 
            px-2 md:px-8 my-4 py-4  min-h-72 w-full sm:w-kilo lg:w-mega shadow-innerW text-white opacity-95`}>

                <article className={"ml-4 flex flex-col h-full w-11/12"}>

                    <h4 className={"font-semibold bg-midnight text-gray-100 px-2 cursor-pointer pt-1.5 "}>{this.props.communityName}</h4>

                    <span className={"mt-2 border rounded p-2 shadow-innerW"}>{this.props.content}</span>

                    {(this.state.picture !== null)?<figure className={"mt-2 border rounded p-2 shadow-innerW max-w-xl"}>
                        <img src={this.state.picture} alt="post"/>
                    </figure>: ''}


                    <p className={"self-end border border-opacity-50 border-l-0 border-r-0 mt-1 text-sm"}>{this.props.creationDate}</p>

                    <div className={"self-center mt-12 flex flex-row xl:mt-24 "}>

                        <Menu
                            anchorEl={this.state.anchorEl}
                            keepMounted
                            open={Boolean(this.state.anchorEl)}
                            onClose={this.handleClose}
                        >
                            <MenuItem onClick={() => {
                                this.setState({commentModal: true});
                                this.handleClose();
                            }}>Responder</MenuItem>
                            <MenuItem onClick={() => {
                                this.setState({repliesModal: true});
                                this.handleClose();
                            }}>Ver respuestas</MenuItem>
                        </Menu>
                        <div aria-controls="simple-menu" aria-haspopup="true" onClick={this.handleClick}
                            className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>

                            <button className={"focus:outline-none"}><img src={atIcon} alt="Responder"/></button>
                            <span className={"bold text-gray-800"}>{this.state.replies.length}</span>
                        </div>

                        <div onClick={() => {

                            this.handleReaction(1);

                        }}
                            className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button className={"focus:outline-none"}><img src={likeIcon} alt="Reaction"/></button>
                            <span className={"bold text-gray-800"}>{this.state.heartReactions.toString()}</span>
                        </div>

                        <div onClick={() => {

                            this.handleReaction(2);

                        }}

                            className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button className={"focus:outline-none"}><img src={amazingIcon} alt="Amazing"/></button>
                            <span className={"bold text-gray-800"}>{this.state.thunderReactions.toString()}</span>
                        </div>

                        <div onClick={() => {

                            this.handleReaction(3);

                        }}

                            className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button className={"focus:outline-none"}><img src={dislikeIcon} alt="Dislike"/></button>
                            <span className={"bold text-gray-800"}>{this.state.thumbDownReactions.toString()}</span>
                        </div>
                    </div>
                </article>

                <aside className={"mr-4 mx-auto"}>
                    <figure className={"w-32 md:shadow-innerW bg-gray-900 md:h-48 md:px-4 md:pt-6 rounded"}>
                        <img className={"rounded-full md:shadow-white"} src="https://images.unsplash.com/photo-1506956191951-7a88da4435e5?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1267&q=80" alt="avatar"/>
                        <figcaption className={"md:mt-4 mx-auto text-center w-100"}>{this.props.userName}</figcaption>
                        <p className={"text-xs text-center mb-2 md:mb-0 md:mt-2 underline cursor-pointer"}>@{this.props.userNickname}</p>
                    </figure>

                </aside>

                <Modal open={this.state.commentModal}>
                    {<CommentBox id={this.props.id}
                                 communityId={this.props.communityId}
                                 handleToggle={this.handleCommentChange}
                                 publish={this.commentPublish}/>}
                </Modal>

                <Modal open={this.state.repliesModal}>
                    {
                        <div className={`flex flex-col items-center bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW
                        overflow-y-scroll`}>
                            <button onClick={()=>this.setState({repliesModal: false})} className={"transform scale-150 ml-8 text-white self-start"}>
                                <img src={backIcon} alt="Back"/>
                            </button>
                            <Feed className={"mx-auto"} posts={this.state.replies}/>
                        </div>
                    }
                </Modal>
            </section>
        );
    }

}

export default PostView;
