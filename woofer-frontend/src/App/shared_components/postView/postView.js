import React from 'react';
import atIcon from '../../../img/icon/at-symbol.svg';
import likeIcon from '../../../img/icon/heart.svg';
import dislikeIcon from '../../../img/icon/thumb-down.svg';
import amazingIcon from '../../../img/icon/lightning-bolt.svg';
import {devRootURL, postPictureApiURLs} from "../../constants/api-url";
import firebase from "firebase";
import {Modal} from "@material-ui/core";
import CommentBox from "../commentBox/commentBox";


class PostView extends React.Component{

    replies = ['','',''];

    state = {

        picture: null,
        comment: false

    }

    constructor(props) {
        super(props);

        let nullPicture = false;
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


    render() {

        return(
            <section className={`border rounded border-white flex flex-col-reverse md:flex-row-reverse justify-end bg-dark pt-8 
            px-2 md:px-8  my-4  min-h-72 w-full sm:w-kilo lg:w-mega shadow-innerW text-white opacity-95`}>

                <article className={"ml-4 flex flex-col h-full w-11/12"}>

                    <h4 className={"font-semibold bg-midnight text-gray-100 px-2 cursor-pointer pt-1.5 "}>{this.props.communityName}</h4>

                    <span className={"mt-2 border rounded p-2 shadow-innerW"}>{this.props.content}</span>

                    {(this.state.picture !== null)?<figure className={"mt-2 border rounded p-2 shadow-innerW max-w-xl"}>
                        <img src={this.state.picture} alt="post"/>
                    </figure>: ''}


                    <p className={"self-end border border-opacity-50 border-l-0 border-r-0 mt-1 text-sm"}>{this.props.creationDate}</p>

                    <div className={"self-center mt-12 flex flex-row xl:mt-24 "}>
                        <div onClick={()=>{

                            this.setState({comment: true})

                        }}
                            className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={atIcon} alt="Responder"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
                        </div>

                        <div className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={likeIcon} alt="Reaction"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
                        </div>

                        <div className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={amazingIcon} alt="Amazing"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
                        </div>

                        <div className={"ml-6 p-1.5 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={dislikeIcon} alt="Dislike"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
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

                <Modal open={this.state.comment}>
                    {<CommentBox id={this.props.id} communityId={this.props.communityId}/>}
                </Modal>
            </section>
        );
    }

}

export default PostView;
