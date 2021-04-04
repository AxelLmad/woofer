import React from 'react';
import PostView from "../postView/postView";

class Feed extends React.Component{



    render(){

        return(
            <section>
                <ul>

                    {this.props.posts.map((post) => {

                        return (<li>
                            <PostView
                            className={"min-w-full"}
                            communityName={post.community.name}
                            content={post.content}
                            creationDate={post.creationDate}
                            userName={post.author.name}
                            userNickname={post.author.nickname}/>
                        </li>)

                    })}

                </ul>


            </section>
        );
        ;

    }

}

export default Feed;
