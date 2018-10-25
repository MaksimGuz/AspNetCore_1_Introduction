import React, { Component } from 'react';

export class FetchProducts extends Component {
    displayName = FetchProducts.name

    static renderProductsTable(prodList) {
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
                </tr>
            </thead>
            <tbody>
                {prodList.map(prod =>
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
                    </tr>
                )}
            </tbody>
        </table>;
    }

    constructor(props) {
        super(props);
        this.state = { prodList: [], loading: true };

        fetch('api/products')
            .then(response => response.json())
            .then(data => {
                this.setState({ prodList: data, loading: false });
            });
    }

    render() {
        let contents = this.state.loading ?
            <p><em>Loading...</em></p> :
            FetchProducts.renderProductsTable(this.state.prodList);
        return (
            <div>
                <h2>Products</h2>
                {contents}
            </div>
        );
    }
}