import {DataGrid, GridColDef, GridToolbarContainer} from "@mui/x-data-grid";
import {useGetUserTestQuery} from "../services/userApi";
import {AiOutlineEdit, AiOutlinePlus, MdOutlineDeleteForever} from "react-icons/all";
import {IconButton} from "@mui/material";
import {Button} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import {useDialog} from "../hooks/useDialog";
import {CreateTestDialog} from "./TestEditForm/CreateTestDialog";
import {UpdateTestDialog} from "./TestEditForm/UpdateTestDialog";
import {ITest} from "../models/ITest";
import {useState} from "react";
import {useDeleteTestMutation} from "../services/testApi";

interface IUserTestListProps {
    userId: number
}

export const UserTestList = ({userId}: IUserTestListProps) => {
    const {data: tests = []} = useGetUserTestQuery(userId)
    const [detete] = useDeleteTestMutation()
    const [testOnEdit, setTestOnEdit] = useState<ITest>({} as ITest)
    const createDialog = useDialog()
    const updateDialog = useDialog()

    const columns: GridColDef[] = [
        { field: "id", headerName: "ID", width: 70 },
        { field: "name", headerName: "Название", width: 400 },
        {
            field: "edit-button",
            headerName: "",
            sortable: false,
            width: 60,
            renderCell: props => (
                <IconButton
                    className="d-flex mx-auto"
                    onClick={() => {
                        setTestOnEdit(props.row as ITest)
                        updateDialog.handleOpen()
                    }}
                >
                    <AiOutlineEdit
                        size="1em"
                    />
                </IconButton>
            )
        },
        {
            field: "delete-button",
            headerName: "",
            sortable: false,
            width: 60,
            renderCell: props => (
                <IconButton
                    className="d-flex mx-auto"
                    onClick={async () => {
                        await detete((props.row as ITest).id)
                    }}
                >
                    <MdOutlineDeleteForever
                        size="1em"
                    />
                </IconButton>
            )
        }
    ]

    const toolbar = () => (
        <GridToolbarContainer style={{ padding: "10px" }}>
            <Button onClick={createDialog.handleOpen}>
                Добавить тест
            </Button>
        </GridToolbarContainer>
    )

    return (
        <div
            style={{
                height: "100%",
                width: "100%"
            }}
        >
            <DataGrid
                columns={columns}
                rows={tests}
                initialState={{
                    columns: {
                        columnVisibilityModel: {
                            id: false,
                        },
                    },
                }}
                components={{ Toolbar: toolbar }}
                disableColumnMenu
                disableRowSelectionOnClick
            />
            <CreateTestDialog
                open={createDialog.open}
                onClose={createDialog.handleClose}
            />
            <UpdateTestDialog
                test={testOnEdit}
                open={updateDialog.open}
                onClose={updateDialog.handleClose}
            />
        </div>
    )
}