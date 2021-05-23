import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App/app';
import firebase from "firebase";
import {firebaseConfig} from "./App/constants/keys";

const fApp = firebase.initializeApp(firebaseConfig);
console.log(fApp);
ReactDOM.render(
  <App/>,
  document.getElementById('root')
);
