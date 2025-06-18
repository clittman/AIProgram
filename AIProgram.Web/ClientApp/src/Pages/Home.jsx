import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';
import axios from 'axios';

const Home = () => {

    const [url, setUrl] = useState('');
    const [summary, setSummary] = useState('');
    const [loading, setLoading] = useState(false);

    const onButtonClick = async () => {
        setLoading(true);
        setSummary('');
        const { data } = await axios.get(`/api/home/getsummary?url=${url}`);
        setSummary(data);
        setLoading(false);
    }

    return (
        <div className="min-vh-100 d-flex flex-column justify-content-center align-items-center text-white" style={{ background: 'linear-gradient(135deg, rgb(102, 126, 234) 0%, rgb(118, 75, 162) 100%)', padding: '2rem' }}>
            <h1 className="mb-4 fw-bold text-center">📰 AI Article Summarizer</h1>
            <div className="card p-4 shadow-lg rounded-4" style={{ maxWidth: '600px', width: '100%' }}>
                <div className="form-group mb-3">
                    <label className="form-label fw-semibold">Paste Article URL:</label>
                    <input type="text" className="form-control" placeholder="https://example.com/news/article" value={url} onChange={e => setUrl(e.target.value)} />
                </div>
                <button className="btn btn-primary w-100 fw-bold" onClick={onButtonClick} disabled={loading}>{loading ? 'Summarizing' : 'Summarize Article'}</button>
            </div>
            {summary && <div className="card mt-4 p-4 shadow rounded-4 bg-light text-dark" style={{maxWidth: '700px', width: '100%'}}>
                <h5 className="fw-bold mb-3">📝 Summary:</h5>
                <p className="mb-0">{summary}</p>
            </div>}
        </div>
    );
};

export default Home;