import React from "react";
import { Redirect } from "react-router-dom";
import PropTypes from "prop-types";
import { useKeycloak } from "@react-keycloak/web";

const GuestGuard = ({ children }) => {
  const { keycloak, initialized } = useKeycloak();

  if (keycloak.authenticated) {
    return <Redirect to="/protected" />;
  }

  return <>{children}</>;
};

GuestGuard.propTypes = {
  children: PropTypes.node,
};

export default GuestGuard;
