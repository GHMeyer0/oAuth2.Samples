import React, { Component } from 'react';
import UserInfo from './UserInfo';
import Logout from './Logout';
import QueryAPI from './QueryAPI';
import Keycloak from 'keycloak-js';
import Permissions from './Permissions';
import KeycloakAuthorization from 'keycloak-authz'

class Secured extends Component {

  constructor(props) {
    super(props);
    this.state = { keycloak: null, authenticated: false, authorization: null };
  }

  componentDidMount() {
    const keycloak = Keycloak('/keycloak.json');
    keycloak.init({ onLoad: 'login-required', checkLoginIframe: false }).then(authenticated => {
      const authorization = new KeycloakAuthorization(keycloak);
      authorization.init();
      this.setState({ keycloak: keycloak, authenticated: authenticated, authorization: authorization })
    })


  }

  render() {
    if (this.state.keycloak) {
      if (this.state.authenticated) return (
        <div>
          <p>
            This is a Keycloak-secured component of your application. You shouldn't be able to see this
            unless you've authenticated with Keycloak.
          </p>
          <UserInfo keycloak={this.state.keycloak} />
          <QueryAPI keycloak={this.state.keycloak} />
          <Logout keycloak={this.state.keycloak} />
          <Permissions keycloak={this.state.keycloak} authorization={this.state.authorization}></Permissions>
        </div>
      ); else return (<div>Unable to authenticate!</div>)
    }
    return (
      <div>Initializing Keycloak...</div>
    );
  }
}

export default Secured;
