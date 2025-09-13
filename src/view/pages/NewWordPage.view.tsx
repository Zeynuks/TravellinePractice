import {useNavigate} from "react-router-dom";
import {Box, Button, Stack, Typography} from "@mui/material";
import {WordForm} from "../components/WordForm/WordForm.tsx";

export const NewWordPageView = () => {
    const navigate = useNavigate();

    return (
        <Box>
            <Stack direction="row" sx={{gap: 2, mb: 4}}>
                <Button variant="outlined" onClick={() => navigate(-1)} sx={{mr: 1}}>
                    {"<"}
                </Button>
                <Typography variant="h3" sx={{color: "#364963"}}>
                    Добавление слова
                </Typography>
            </Stack>
            <WordForm/>
        </Box>
    );
};