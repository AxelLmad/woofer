import React from 'react';
import backIcon from "../../../img/icon/arrow-left.svg";
import SmallListCard from "./small-list-card";
import {User} from '../../../models/user';


class SmallList extends React.Component{

    constructor() {
        super();

        this.toggleOpenModal = this.toggleOpenModal.bind(this);
    }

    state = {
        openModal: false
    }

    toggleOpenModal(flag = undefined){


        if(flag === undefined){

            this.setState({

                openModal: !this.state.openModal

            });

        }

        else{

            this.setState({

                openModal: flag

            });
        }

    }

    renderCards() {
        let key = 0;
        return this.props.list.map((el) => {

            return (<li key={key++}><SmallListCard id={el.id} name={el.name}
                                   type={(el instanceof User)?'user':'community'}/></li>);

    });
    }

    render(){

        return <div>

            <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>

                    <button onClick={()=>this.toggleOpenModal()} className={" text-white "}>
                        <img src={backIcon} alt="Back"/>
                    </button>

                <div className={"flex flex-col"}>
                    <ul>
                        {this.renderCards()}
                    </ul>
                </div>

            </section>

                </div>;

    }

}

export default SmallList;
