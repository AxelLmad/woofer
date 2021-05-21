import React from 'react';
import PostView from "../postView/postView";

class Feed extends React.Component{

    renderPosts() {

        let keyCounter = 0;

        return this.props.posts.map((post) => {

            return (
                <li key={keyCounter++}>
                    <PostView
                        className={"min-w-full"}
                        communityName={post.community.name}
                        content={post.content}
                        creationDate={post.creationDate}
                        userName={post.author.name}
                        userNickname={post.author.nickname}/>
                </li>
            );

        })

    }

    render(){
        return(
            <section>
                <ul>
                    {this.renderPosts()}
                </ul>

            </section>
        );
        ;

    }

}

export default Feed;
