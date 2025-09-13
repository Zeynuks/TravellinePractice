import {Box, Stack, Typography} from "@mui/material";
import {BackButton} from "../components/BackButton/BackButton.tsx";
import {WordForm} from "../components/WordForm/WordForm.tsx";
import type {EditWordPageProps} from "./EditWordPage.tsx";

export const EditWordPageView = ({id, word}: EditWordPageProps) => {

    return (
        <Box>
            <Stack direction="row" sx={{gap: 2, mb: 3}}>
                <BackButton/>
                <Typography variant="h3">
                    Редактирование слова
                </Typography>
            </Stack>
            <WordForm id={id} word={word}/>
        </Box>
    );
};