import classes from "./Loader.module.scss";

export type LoaderProps = {
    message?: string;
}

export const Loader = ({message}: LoaderProps) => {
    return (
      <>
          <div className={classes.boxes}>
              <div className={classes.box}>
                  <div></div>
                  <div></div>
                  <div></div>
                  <div></div>
              </div>
              <div className={classes.box}>
                  <div></div>
                  <div></div>
                  <div></div>
                  <div></div>
              </div>
              <div className={classes.box}>
                  <div></div>
                  <div></div>
                  <div></div>
                  <div></div>
              </div>
              <div className={classes.box}>
                  <div></div>
                  <div></div>
                  <div></div>
                  <div></div>
              </div>
          </div>
          <span className={classes.message}>{message}</span>
      </>
);
}
