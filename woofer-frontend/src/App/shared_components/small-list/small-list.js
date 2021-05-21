import React from 'react';
import backIcon from "../../../img/icon/arrow-left.svg";


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

    render(){

        return <div>

            <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>

                    <button onClick={()=>this.toggleOpenModal()} className={" text-white "}>
                        <img src={backIcon} alt="Back"/>
                    </button>

                <div className={"flex flex-col"}>
                    <ul>
                        <li>
                            {this.props.list.map((el) => {

                                return (<article className={"flex flex-col items-center bg-dark w-full"}>
                                    <h5>{el.name}</h5>
                                    <figure><img className={"w-1/3 mx-auto"} src={el.picture} alt={el.type}/></figure>
                                    <button onClick={this.handleSeguirButton}
                                            className={"rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                                        Seguir
                                    </button>
                                </article>)
                            })}
                        </li>
                    </ul>
                </div>

            </section>

                </div>;

    }

}

export default SmallList;
