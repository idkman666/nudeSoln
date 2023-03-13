import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import HomePage from './components/HomePage';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <HomePage>
        </HomePage>
    );
  }
}
