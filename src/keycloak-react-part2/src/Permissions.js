import React, { useEffect } from 'react';


const Permissions = (props) => {
  useEffect(() => {
    props.authorization.entitlement('exemple-api').then((res) => console.log(res));
  }, [])
  return (
    <div>
      {props.authorization.rpt}
    </div>
  )
}

export default Permissions
