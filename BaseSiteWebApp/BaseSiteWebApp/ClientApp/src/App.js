import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchCategories } from './components/FetchCategories';
import { EditCategoryImage } from './components/EditCategoryImage';
import { FetchProducts } from './components/FetchProducts';
import { EditProduct } from './components/EditProduct';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/fetchcategories' component={FetchCategories} />
                <Route path='/fetchproducts' component={FetchProducts} />
                <Route path='/editcategoryimage/:catid' component={EditCategoryImage} />
                <Route exact path='/editproduct' component={EditProduct} /> 
                <Route path='/editproduct/:prodid' component={EditProduct} />
            </Layout>
        );
    }
}
