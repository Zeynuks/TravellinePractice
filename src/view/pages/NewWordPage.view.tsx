import {useNavigate} from "react-router-dom";
import {Button, Stack, Typography} from "@mui/material";
import {WordForm} from "../components/WordForm/WordForm.tsx";

export const NewWordPageView = () => {
    const navigate = useNavigate();

    return (
        <>
            <Stack direction="row" sx={{gap: 2, mb: 3}}>
                <Button variant="outlined" onClick={() => navigate(-1)} sx={{mr: 1}}>
                    {"<"}
                </Button>
                <Typography variant="h3">
                    Добавление слова
                </Typography>
            </Stack>
            <WordForm/>
        </>
    );
};