import React from 'react';

export const ErrorLog = (props) => {

    if (props.messages === null || props.messages === undefined || props.messages.length <= 0){

        return '';

    }
    else{

        return( <div className={"text-danger border border-danger xl:w-1/3 mx-auto px-4 py-2"}>
            {props.messages.map((element) => {return <p key={element}>{element}</p>})}
        </div> );

    }


};
