import React from 'react';
import atIcon from '../../../img/icon/at-symbol.svg';
import likeIcon from '../../../img/icon/heart.svg';
import dislikeIcon from '../../../img/icon/thumb-down.svg';
import amazingIcon from '../../../img/icon/lightning-bolt.svg';


class PostView extends React.Component{

    replies = ['','',''];

    render() {

        return(
            <section className={`border border-white flex flex-col-reverse md:flex-row-reverse justify-end bg-dark pt-8 
            px-2 md:px-8  my-4  min-h-72 w-full sm:w-kilo lg:w-mega shadow-innerW text-white opacity-95`}>

                <article className={"ml-4 flex flex-col h-full w-11/12"}>

                    <h4 className={"font-semibold bg-midnight text-gray-100 px-2 cursor-pointer pt-1.5 "}>{this.props.communityName}</h4>

                    <span className={"mt-2 border p-2 shadow-innerW"}>{this.props.content}</span>

                    <p className={"self-end border border-opacity-50 border-l-0 border-r-0 mt-1 text-sm"}>{this.props.creationDate}</p>

                    <div className={"self-center mt-12 flex flex-row xl:mt-24 "}>
                        <div className={"ml-6 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={atIcon} alt="Responder"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
                        </div>

                        <div className={"ml-6 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={likeIcon} alt="Reaction"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
                        </div>

                        <div className={"ml-6 rounded-full hover:bg-light cursor-pointer"}>
                            <button><img src={amazingIcon} alt="Amazing"/></button>
                            <span className={"bold text-gray-800"}>{this.replies.length}</span>
                        </div>

                        <div className={"ml-6 rounded-full hover:bg-light cursor-pointer"}>
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

            </section>
        );
    }

}

export default PostView;
