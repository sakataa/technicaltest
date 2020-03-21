import React from 'react';
import {
  Navbar,
  NavbarBrand
} from 'reactstrap';

const Header = () => {
  return (
    <div>
      <Navbar color="secondary" light expand="md">
        <NavbarBrand className="text-white" href="/">Covid-19</NavbarBrand>
      </Navbar>
    </div>
  );
}

export default Header;