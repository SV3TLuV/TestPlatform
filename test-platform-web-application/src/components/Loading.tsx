import {Spinner} from "react-bootstrap";

export const Loading = () => {
    return (
        <div
            className="d-flex align-items-center justify-content-center"
            style={{
                height: "calc(100vh - 74px)",
            }}
        >
            <Spinner/>
        </div>
    )
}