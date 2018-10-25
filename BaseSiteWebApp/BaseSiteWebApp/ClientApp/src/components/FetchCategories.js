import React, { Component } from 'react';

export class FetchCategories extends Component {
    displayName = FetchCategories.name

    static renderCategoriesTable(catlist) {
        return <table className="table">
            <thead>
                <tr>
                    <th />
                    <th>CategoryName</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
                {catlist.map(cat =>
                    <tr key={cat.categoryId}>
                        <td />
                        <td>{cat.categoryName}</td>
                        <td>{cat.description}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    constructor(props) {
        super(props);
        this.state = { catlist: [], loading: true };

        fetch('api/categories')
            .then(response => response.json())
            .then(data => {
                this.setState({ catlist: data.categories, loading: false });
            });
    }

    render() {
        let contents = this.state.loading ?
            <p><em>Loading...</em></p> :
            FetchCategories.renderCategoriesTable(this.state.catlist);            
        return (
            <div>
                <h2>Categories</h2>
                {contents}
            </div>
        );
    }
}