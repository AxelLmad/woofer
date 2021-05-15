import React from 'react';
import {Post} from "../../models/post";
import Feed from "../shared_components/feed/feed";
import Publisher from "../shared_components/publisher/publisher";

class Home extends React.Component{

    constructor() {
        super();

        const userNickname = JSON.parse(localStorage.getItem('woofer-user-ac'));

        if (userNickname === null || userNickname === undefined || userNickname.acc === ''){

            window.location.href = 'http://localhost:3000/login';

        }
    }

    posts = [
        new Post(0, 'Is naruto the besta anime ever?', '03-04-2021', {name: 'Jane Doe', nickname: 'janedoe'}, {name: 'Naruto'}),
        new Post(1, 'Is Lapras the next pokemon in smash?', '03-04-2021', {name: 'Sad Togeppi', nickname: 'togeppi'}, {name: 'Pokemon'}),
    ];

    render(){

        return(

                <div className={"flex flex-col w-full items-center mt-16"}>
                    <Publisher username={JSON.parse(localStorage.getItem('woofer-user-ac')).acc}/>
                    <Feed posts={this.posts}/>
                </div>

        );

    }

}

export default Home;
