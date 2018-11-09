import React, { Component } from 'react';
import axios from 'axios';

export class FetchProducts extends Component {
    displayName = FetchProducts.name

    constructor(props) {
        super(props);
        this.state = { prodList: [], loading: true };

        axios.get('api/apiproducts/')
            .then(res => {
                this.setState({ prodList: res.data, loading: false });
            });
        // This binding is necessary to make "this" work in the callback
        this.handleEdit = this.handleEdit.bind(this);
        this.handleCreate = this.handleCreate.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
    }

    handleEdit(id) {
        this.props.history.push("/editproduct/" + id);
    }

    handleCreate() {
        this.props.history.push("/editproduct");
    }

    handleDelete(id) {
        axios.delete('api/apiproducts/' + id)
            .then(() => this.setState(
                {
                    prodList: this.state.prodList.filter((rec) => {
                        return rec.productId !== id;
                    })
                }));
    }

    renderProductsTable(prodList: ProductData[]) {
        return <table className="table">
            <thead>
                <tr>
                    <th />
                    <th>ProductName</th>
                    <th>QuantityPerUnit</th>
                    <th>UnitPrice</th>
                    <th>UnitsInStock</th>
                    <th>UnitsOnOrder</th>
                    <th>ReorderLevel</th>
                    <th>Category</th>
                    <th>Supplier</th>
                    <th />
                    <th />
                </tr>
            </thead>
            <tbody>
                {this.state.prodList.map(prod =>
                    <tr key={prod.productId}>
                        <td />
                        <td>{prod.productName}</td>
                        <td>{prod.quantityPerUnit}</td>
                        <td>{prod.unitPrice}</td>
                        <td>{prod.unitsInStock}</td>
                        <td>{prod.unitsOnOrder}</td>
                        <td>{prod.reorderLevel}</td>
                        <td>{prod.category.categoryName}</td>
                        <td>{prod.supplier.companyName}</td>
                        <td><a className="action" onClick={(id) => this.handleEdit(prod.productId)}>Edit</a> </td>
                        <td><a className="action" onClick={(id) => this.handleDelete(prod.productId)}>Delete</a> </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    render() {
        let contents = this.state.loading ?
            <p><em>Loading...</em></p> :
            this.renderProductsTable(this.state.prodList);
        return (
            <div>
                <h2>Products</h2>
                <p>
                    <a className="btn btn-primary" onClick={this.handleCreate}>Create New</a>
                </p>
                {contents}
            </div>
        );
    }
}

export class ProductData {
    productId = 0;
    productName = "";
    supplierId = 0;
    categoryId = 0;
    quantityPerUnit = "";
    unitPrice = 0.0;
    unitsInStock = 0;
    unitsOnOrder = 0;
    reorderLevel = 0;
    discontinued = false;
}