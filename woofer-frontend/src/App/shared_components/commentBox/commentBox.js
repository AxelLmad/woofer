import React from 'react';
import backIcon from "../../../img/icon/arrow-left.svg";
import {devRootURL, postApiURLs} from "../../constants/api-url";
import {lsUserKey} from "../../constants/keys";

class CommentBox extends React.Component{

    state = {

        content: ''
    }

    constructor(props) {
        super(props);
        this.publish = this.publish.bind(this);
        this.handleChangeContent = this.handleChangeContent.bind(this);
    }

    handleChangeContent($e) {

        this.setState({content: $e.target.value});

    }

    publish(){

        const acc = JSON.parse(localStorage.getItem(lsUserKey));

        if (acc === null || acc === undefined){

            window.location.href = 'http://localhost:3000/register';
            return;

        }

        const postBody = JSON.stringify(
            {
                content: this.state.content,
                authorId: acc.id,
                communityId: this.props.communityId,
                lastPostId: this.props.id,
                serverPathImg: null
            }
        );
        fetch(`${devRootURL}${postApiURLs.create}`, {
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => {
                if (response.status === 200) {

                    this.toggleOpenModal(false);

                }
            })
            .catch(err => console.log(err));

    }

    render(){

        return <div>

            <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>

                <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                    <button onClick={this.props.handleToggle} className={"transform scale-150 ml-8 text-white "}>
                        <img src={backIcon} alt="Back"/>
                    </button>
                    <button onClick={() => {

                        const acc = JSON.parse(localStorage.getItem(lsUserKey));

                        this.props.publish(JSON.stringify({
                            content: this.state.content,
                            authorId: acc.id,
                            communityId: this.props.communityId,
                            lastPostId: this.props.id,
                            serverPathImg: null

                        }))

                    }}
                            className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Publicar</button>
                </div>

                <textarea onChange={this.handleChangeContent}
                    value={this.state.content} className={"w-2/3 h-5/6 mx-auto bg-gray-900 py-4 px-2 resize-none"}></textarea>

            </section>

        </div>;

    }

}

export default CommentBox;
