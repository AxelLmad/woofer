import React from 'react';
import {Post} from "../../models/post";
import Feed from "../shared_components/feed/feed";
import Layout from "../layout/layout";

class Home extends React.Component{

    posts = [
        new Post(0, 'Is naruto the besta anime ever?', '03-04-2021', {name: 'Jane Doe', nickname: 'janedoe'}, {name: 'Naruto'}),
        new Post(1, 'Is Lapras the next pokemon in smash?', '03-04-2021', {name: 'Sad Togeppi', nickname: 'togeppi'}, {name: 'Pokemon'}),
    ];

    render(){

        return(
            <Layout>
                <div className={"flex flex-col w-full items-center mt-16"}>
                    <Feed posts={this.posts}/>
                </div>
            </Layout>
        );

    }

}

export default Home;
