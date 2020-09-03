import React from "react";
import { Redirect } from "react-router-dom";
import PropTypes from "prop-types";
import { useKeycloak } from "@react-keycloak/web";

const AuthGuard = ({ children }) => {
  const { keycloak, initialized } = useKeycloak();

  if (!keycloak.authenticated) {
    return <Redirect to="/login" />;
  }

  return <>{children}</>;
};

AuthGuard.propTypes = {
  children: PropTypes.node,
};

export default AuthGuard;
